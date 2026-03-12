"""Element finder for locating and updating elements by ID or other criteria."""

from __future__ import annotations

from typing import Any, Callable, TypeVar

from ..models.elements import Element

E = TypeVar("E", bound=Element)


class ElementFinder:
    """Element finder for locating and updating elements within a card.

    Provides methods to find elements by ID, tag, or other criteria.
    Also supports updating elements by ID.

    示例:
        >>> from lark_card_kit import CardBuilder, ElementFinder
        >>>
        >>> # 创建卡片
        >>> with CardBuilder.create() as builder:
        ...     with builder.body() as b:
        ...         b.plain_text("Hello")
        ...         with b.button() as btn:
        ...             btn.text("Click").element_id("btn1")
        >>>
        >>> card = builder.build()
        >>>
        >>> # 查找并修改组件
        >>> finder = ElementFinder(card.body.elements)
        >>> button = finder.find_by_id("btn1")
        >>> if button:
        ...     button.disabled = True
        >>>
        >>> # 使用 update_by_id 便捷方法
        >>> finder.update_by_id("btn1", lambda e: setattr(e, "disabled", True))
    """

    def __init__(self, elements: list[Element]) -> None:
        """Initialize the element finder.

        Args:
            elements: List of elements to search.
        """
        self._elements = elements

    def find_by_id(self, element_id: str) -> Element | None:
        """Find an element by its ID.

        Args:
            element_id: Element ID to search for.

        Returns:
            Element if found, None otherwise.
        """
        return self._find_by_id_recursive(self._elements, element_id)

    def find_by_id_as(self, element_id: str, element_type: type[E]) -> E | None:
        """Find an element by its ID and cast to specific type.

        Args:
            element_id: Element ID to search for.
            element_type: Expected element type.

        Returns:
            Element if found and of correct type, None otherwise.
        """
        element = self.find_by_id(element_id)
        if isinstance(element, element_type):
            return element
        return None

    def update_by_id(self, element_id: str, updater: Callable[[Element], None]) -> bool:
        """Find an element by ID and update it.

        Args:
            element_id: Element ID to search for.
            updater: Function to update the element.

        Returns:
            True if element was found and updated, False otherwise.

        示例:
            >>> finder.update_by_id("btn1", lambda e: setattr(e, "disabled", True))
        """
        element = self.find_by_id(element_id)
        if element is not None:
            updater(element)
            return True
        return False

    def update_by_id_as(
        self,
        element_id: str,
        element_type: type[E],
        updater: Callable[[E], None],
    ) -> bool:
        """Find an element by ID, cast to specific type, and update it.

        Args:
            element_id: Element ID to search for.
            element_type: Expected element type.
            updater: Function to update the element.

        Returns:
            True if element was found, of correct type, and updated, False otherwise.

        示例:
            >>> from lark_card_kit import Button
            >>> finder.update_by_id_as("btn1", Button, lambda b: b.disabled = True)
        """
        element = self.find_by_id_as(element_id, element_type)
        if element is not None:
            updater(element)
            return True
        return False

    def _find_by_id_recursive(self, elements: list[Element], element_id: str) -> Element | None:
        """Recursively search for an element by ID.

        Args:
            elements: List of elements to search.
            element_id: Element ID to search for.

        Returns:
            Element if found, None otherwise.
        """
        for element in elements:
            if getattr(element, "element_id", None) == element_id:
                return element

            # Check nested elements
            nested = self._get_nested_elements(element)
            if nested:
                found = self._find_by_id_recursive(nested, element_id)
                if found:
                    return found

        return None

    def find_by_tag(self, tag: str) -> list[Element]:
        """Find all elements with a specific tag.

        Args:
            tag: Element tag to search for.

        Returns:
            List of matching elements.
        """
        return self._find_by_tag_recursive(self._elements, tag)

    def _find_by_tag_recursive(self, elements: list[Element], tag: str) -> list[Element]:
        """Recursively search for elements by tag.

        Args:
            elements: List of elements to search.
            tag: Element tag to search for.

        Returns:
            List of matching elements.
        """
        result: list[Element] = []

        for element in elements:
            if getattr(element, "tag", None) == tag:
                result.append(element)

            # Check nested elements
            nested = self._get_nested_elements(element)
            if nested:
                result.extend(self._find_by_tag_recursive(nested, tag))

        return result

    def update_all_by_tag(self, tag: str, updater: Callable[[Element], None]) -> int:
        """Find all elements with a specific tag and update them.

        Args:
            tag: Element tag to search for.
            updater: Function to update each element.

        Returns:
            Number of elements updated.
        """
        elements = self.find_by_tag(tag)
        for element in elements:
            updater(element)
        return len(elements)

    def _get_nested_elements(self, element: Element) -> list[Element]:
        """Get nested elements from a container element.

        Args:
            element: Container element.

        Returns:
            List of nested elements.
        """
        # Check for common container properties
        if hasattr(element, "elements") and isinstance(element.elements, list):
            return element.elements
        if hasattr(element, "columns") and isinstance(element.columns, list):
            result: list[Element] = []
            for column in element.columns:
                if hasattr(column, "elements"):
                    result.extend(column.elements)
            return result

        return []
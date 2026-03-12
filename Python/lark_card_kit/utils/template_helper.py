"""Template helper for card-level template filling."""

from __future__ import annotations

from typing import Any

from ..models.card import Card
from ..models.elements import (
    Element,
    PlainText,
    Markdown,
    MarkdownText,
    ColumnSet,
    Form,
    CollapsiblePanel,
    Loop,
)
from ..templates.template_filler import TemplateParameterFiller


def fill_template(card: Card, filler: TemplateParameterFiller) -> None:
    """Fill template parameters in a card.

    Args:
        card: Card to fill template parameters in.
        filler: Template parameter filler.
    """
    if card.header:
        _fill_template_in_element(card.header.title, filler)
        _fill_template_in_element(card.header.subtitle, filler)

    _fill_template_in_elements(card.body.elements, filler)


def fill_template_with_params(card: Card, parameters: dict[str, Any]) -> None:
    """Fill template parameters in a card using a parameter dictionary.

    Args:
        card: Card to fill template parameters in.
        parameters: Parameter dictionary.
    """
    filler = TemplateParameterFiller()
    filler.set_parameters(parameters)
    fill_template(card, filler)


def _fill_template_in_element(element: Element | None, filler: TemplateParameterFiller) -> None:
    """Fill template parameters in an element.

    Args:
        element: Element to fill template parameters in.
        filler: Template parameter filler.
    """
    if element is None:
        return

    if isinstance(element, PlainText):
        element.content = filler.fill_string(element.content)
    elif isinstance(element, Markdown):
        element.content = filler.fill_string(element.content)
    elif isinstance(element, MarkdownText):
        element.content = filler.fill_string(element.content)
    elif isinstance(element, ColumnSet):
        for column in element.columns:
            _fill_template_in_elements(column.elements, filler)
    elif isinstance(element, Form):
        _fill_template_in_elements(element.elements, filler)
    elif isinstance(element, CollapsiblePanel):
        if element.header:
            _fill_template_in_element(element.header.title, filler)
        _fill_template_in_elements(element.elements, filler)
    elif isinstance(element, Loop):
        _fill_template_in_element(element.template, filler)


def _fill_template_in_elements(elements: list[Element] | None, filler: TemplateParameterFiller) -> None:
    """Fill template parameters in a list of elements.

    Args:
        elements: List of elements to fill template parameters in.
        filler: Template parameter filler.
    """
    if elements is None:
        return

    for element in elements:
        _fill_template_in_element(element, filler)
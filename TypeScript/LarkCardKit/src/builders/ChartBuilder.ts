import type { Chart } from '../models/elements/Chart';

export class ChartBuilder {
  private chart: Chart = {} as Chart;

  constructor() {
    this.chart.tag = 'chart';
  }

  /**
   * 设置图表类型
   */
  chartType(type: string): ChartBuilder {
    this.chart.chart_type = type;
    return this;
  }

  /**
   * 设置图表数据
   */
  data(data: any): ChartBuilder {
    this.chart.data = data;
    return this;
  }

  /**
   * 设置图表宽度
   */
  width(width: string): ChartBuilder {
    this.chart.width = width;
    return this;
  }

  /**
   * 设置元素 ID
   */
  elementId(id: string): ChartBuilder {
    this.chart.element_id = id;
    return this;
  }

  /**
   * 设置外边距
   */
  margin(margin: string): ChartBuilder {
    this.chart.margin = margin;
    return this;
  }

  /**
   * 构建 Chart 对象
   */
  build(): Chart {
    return this.chart;
  }
}

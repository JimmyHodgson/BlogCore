const apexBarChart = Vue.component('apex-bar-chart', {
    components: {
        apexchart: window.VueApexCharts
    },
    data: function () {
        return {
            options: {
                chart: {
                    foreColor: '#fff',
                    toolbar: {
                        show: false
                    }
                },
                colors: ["#e67e22"],
                dataLabels: {
                    enabled: true,
                    style: {
                        colors: ["#fff"]
                    }
                },
                plotOptions: {
                    bar: {
                        columnWidth: '20%',
                        horizontal: false
                    }
                },
                tooltip: {
                    enabled: false
                },
                xaxis: {
                    categories: []
                }
            },
            series: [
                {
                    name: '',
                    data: []
                }
            ]
        };
    },
    methods: {

    },
    props: {
        title: {
            required: true,
            type: String
        },
        xaxis: {
            required: true,
            type: Array
        },
        yaxis: {
            required: true,
            type: Array
        }
    },
    template:
        `   
            <div class="chart-component">
                <div class="chart-header">
                    <h2 class="title">{{title}}</h2>
                </div>
                <div>
                    <apexchart ref="chart" height="300" type="bar" :options="options" :series="series"></apexchart>
                </div>
            </div>
        `,
    watch: {
        xaxis: function (newVal) {
            this.$refs.chart.updateOptions({
                xaxis: {
                    categories: newVal
                }
            });
        },
        yaxis: function (newVal) {
            this.series = newVal;
        }
    }
});

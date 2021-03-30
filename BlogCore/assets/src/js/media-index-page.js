if (document.getElementById('media-index-page') !== null) {
    new Vue({
        el: "#media-index-page",
        components: {
            storageBarComponent,
            apexBarChart
        },
        data: function () {
            return {
                categories: [],
                chartTitle:"Images per group",
                imagesCount:0,
                groupsCount:0,
                max: 0,
                series: [{name:'Groups',data:[]}],
                used: 0,
                url: '/api/Bucket/GetStorageInfo'
            };
        },
        methods: {
            fillStorageBar(data) {
                this.max = data.maxValue;
                this.used = data.groups.reduce((total, current) => total += current.size, 0);
            },
            fillDisplay(data) {
                this.imagesCount = data.groups.reduce((total, current) => total += current.count, 0);
                this.groupsCount = data.groups.length;
            },
            fillChart(data) {
                let seriesData = [];
                let categories = [];
                data.groups.forEach(group => {
                    categories.push(group.name);
                    seriesData.push(group.count);
                });

                this.categories = categories;
                this.series.data = data;
            }
        },
        mounted() {
            common.get(this.url)
                .then(data => {
                    this.fillStorageBar(data);
                    this.fillDisplay(data);
                    this.fillChart(data);
                })
                .catch(error => console.error(error));
        }
    });
}
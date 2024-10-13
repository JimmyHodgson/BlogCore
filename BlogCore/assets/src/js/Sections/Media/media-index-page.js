if (document.getElementById('media-index-page') !== null) {
    Promise.all([
        import('vue'),
        import('components'),
        import('common')
    ]).then(([{ createApp }, { storageBarComponent, apexBarChart }, { common }]) => {
        createApp({
            components: {
                storageBarComponent,
                apexBarChart
            },
            data: function () {
                return {
                    categories: [],
                    groupsCount: 0,
                    hiddenData: [],
                    imagesCount: 0,
                    loading: false,
                    max: 0,
                    series: [{ name: 'Groups', data: [] }],
                    showHidden: false,
                    used: 0,
                    url: '/api/Bucket/GetStorageInfo'
                };
            },
            methods: {
                fillStorageBar(data) {
                    this.max = data.maxValue;
                    this.used = data.groups.reduce((_total, current) => _total += current.size, 0);
                },
                fillDisplay(data) {
                    this.imagesCount = data.groups.reduce((_total, current) => _total += current.count, 0);
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
                    this.series = [{ name: 'Groups', data: seriesData }];
                },
                load() {
                    this.loading = true;
                    common.get(this.url)
                        .then(data => {
                            if (!this.showHidden) {
                                data.groups = data.groups.filter(group => group.name !== "__thumbnails/");
                            }
                            this.fillStorageBar(data);
                            this.fillDisplay(data);
                            this.fillChart(data);
                        })
                        .catch(error => console.error(error))
                        .finally(() => this.loading = false);
                }
            },
            mounted() {
                this.load();
            },
            watch: {
                showHidden: function () {
                    this.load();
                }
            }
        }).mount("#media-index-page");
    }).catch(error => {
        console.error("Failed to load module: ", error);
    })
}

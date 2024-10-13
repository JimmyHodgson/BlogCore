if (document.getElementById('job-management') !== null) {
    Promise.all([
        import('vue'),
        import('components')
    ]).then(([{ createApp }, { tableComponent }]) => {
        createApp({
            components: {
                tableComponent
            },
            data: function () {
                return {
                    options: {
                        endpoint: '/api/Job',
                        dataset: {
                            actions: {
                                details: { enabled: true, url: 'Details' },
                                edit: { enabled: true, url: 'Edit' },
                                remove: { enabled: true, url: 'Delete' }
                            },
                            hide: ['Id', 'Description']
                        },
                        key: 'Id',
                        ribbon: [{
                            url: 'Create',
                            icon: 'far fa-plus'
                        }]
                    }
                };
            },
            mounted: function () {
            },
            methods: {
                isEmpty() {
                    return !this.data.length > 0;
                }
            }
        }).mount('#job-management');
    }).catch(error => {
        console.error("Failed to load module: ", error);
    })
}

if (document.getElementById('mediagroup-management') !== null) {
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
                        endpoint: '/api/MediaGroup',
                        dataset: {
                            actions: {
                                details: { enabled: true, url: 'Details' },
                                edit: { enabled: true, url: 'Edit' },
                                remove: { enabled: true, url: 'Delete' }
                            },
                            showKey: false,
                            hide: ['Id']
                        },
                        key: 'Id',
                        ribbon: [{
                            icon: 'far fa-plus',
                            url: 'Create'
                        }]
                    }
                };
            }
        }).mount('#mediagroup-management');
    }).catch(error => {
        console.error("Failed to load module: ", error);
    })
}

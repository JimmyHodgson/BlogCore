if (document.getElementById('medialink-management') !== null) {
    Promise.all([
        import('vue'),
        import('components')
    ]).then(([{ createApp }, { galleryComponent }]) => {
        createApp({
            components: {
                galleryComponent
            },
            data: function () {
                return {
                    options: {
                        endpoint: '/api/MediaLink',
                        grouping: {
                            enabled: true,
                            entity: 'Group'
                        },
                        key: 'Id',
                        ribbon: [{
                            icon: 'far fa-plus',
                            url: 'Create'
                        }]
                    }
                };
            }
        }).mount('#medialink-management');
    }).catch(error => {
        console.error("Failed to load module: ", error);
    })
}

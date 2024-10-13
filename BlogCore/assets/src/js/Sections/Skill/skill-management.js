if (document.getElementById('skill-management') !== null) {
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
                        endpoint: '/api/Skill',
                        dataset: {
                            actions: {
                                details: { enabled: true, url: 'Details' },
                                edit: { enabled: true, url: 'Edit' },
                                remove: { enabled: true, url: 'Delete' }
                            },
                            hide: ['Id']
                        },
                        key: 'Id',
                        ribbon: [{
                            url: 'Create',
                            icon: 'far fa-plus'
                        }]
                    }
                };
            },
            methods: {
                isEmpty() {
                    return !this.data.length > 0;
                }
            }
        }).mount('#skill-management');
    }).catch(error => {
        console.error("Failed to load module: ", error);
    })
}

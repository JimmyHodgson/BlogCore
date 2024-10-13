if (document.getElementById('mediagroup-delete-form') !== null) {
    Promise.all([
        import('vue'),
        import('common')
    ]).then(([{ createApp }, { common }]) => {
        createApp({
            data: function () {
                return {
                    errors: {
                        result: null
                    },
                    id: null,
                    loading: false,
                    url: '/api/MediaGroup'
                };
            },
            mounted() {
                this.id = document.getElementById('Id')._value;
            },
            methods: {
                submit() {
                    this.loading = true;
                    common.delete(`${this.url}(${this.id})`)
                        .then(() => window.location.href = "/Mediagroup/Index")
                        .catch(response => { this.errors.result = response.error.message; })
                        .finally(() => this.loading = false);
                }
            }
        }).mount("#mediagroup-delete-form");
    }).catch(error => {
        console.error("Failed to load module: ", error);
    })
}

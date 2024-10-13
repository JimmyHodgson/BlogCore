if (document.getElementById('medialink-delete-form') !== null) {
    Promise.all([
        import('vue'),
        import('components'),
        import('common')
    ]).then(([{ createApp }, { uploadComponent }, { common }]) => {
        createApp({
            components: {
                uploadComponent
            },
            mounted() {
                this.id = document.getElementById('Id')._value;
            },
            data: function () {
                return {
                    errors: {
                        result: null
                    },
                    id: null,
                    loading: false,
                    url: '/api/MediaLink'
                };
            },
            methods: {
                checkForm() {
                    return this.id !== null;
                },
                submit() {
                    if (this.checkForm()) {
                        this.loading = true;

                        common.delete(`${this.url}(${this.id})`)
                            .then(() => window.location.href = "/Medialink/Index")
                            .catch(response => {
                                this.errors.result = response.error.message;
                            })
                            .finally(() => this.loading = false);
                    }
                }
            }
        }).mount("#medialink-delete-form");
    }).catch(error => {
        console.error("Failed to load module: ", error);
    })
}

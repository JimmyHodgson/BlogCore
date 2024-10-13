if (document.getElementById('education-delete-form') !== null) {
    Promise.all([
        import('vue'),
        import('common')
    ]).then(([{ createApp }, { common }]) => {
        createApp({
            mounted() {
                this.id = document.getElementById('Id')._value;
            },
            data: function () {
                return {
                    errors: {
                        result: null
                    },
                    id: '',
                    url: '/api/Education'
                };
            },
            methods: {
                submit() {
                    common.delete(`${this.url}(${this.id})`)
                        .then(() => window.location.href = "/Education/Index")
                        .catch(response => this.errors.result = response.error.message);
                }
            }
        }).mount('#education-delete-form');
    }).catch(error => {
        console.error("Failed to load module: ", error);
    })
}

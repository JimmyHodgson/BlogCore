if (document.getElementById('job-delete-form') !== null) {
    Promise.all([
        import('vue'),
        import('common')
    ]).then(([{ createApp }, { common }]) => {
        createApp({
            mounted() {
                this.id = document.getElementById('Id').value;
            },
            data: function () {
                return {
                    errors: {
                        result: null
                    },
                    id: '',
                    url: '/api/Job'
                };
            },
            methods: {
                submit() {
                    common.delete(`${this.url}(${this.id})`)
                        .then(() => window.location.href = "/Job/Index")
                        .catch(response => this.errors.result = response.error.message);
                }
            }
        }).mount('#job-delete-form');
    }).catch(error => {
        console.error("Failed to load module: ", error);
    })
}

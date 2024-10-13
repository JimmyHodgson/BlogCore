if (document.getElementById('skill-delete-form') !== null) {
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
                    url: '/api/Skill'
                };
            },
            methods: {
                submit() {
                    common.delete(`${this.url}(${this.id})`)
                        .then(() => window.location.href = "/Skill/Index")
                        .catch(response => this.errors.result = response.error.message);
                }
            }
        }).mount('#skill-delete-form');
    }).catch(error => {
        console.error("Failed to load module: ", error);
    })
}

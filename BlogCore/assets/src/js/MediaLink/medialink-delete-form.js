if (document.getElementById('medialink-delete-form') !== null) {
    new Vue({
        el: "#medialink-delete-form",
        components: {
            uploadComponent
        },
        beforeMount() {
            this.id = this.$el.querySelector('#Id').value;
        },
        data: function () {
            return {
                errors: {
                    result: null
                },
                id: null,
                loading:false,
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
    });
}
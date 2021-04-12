if (document.getElementById('skill-delete-form') !== null) {
    new Vue({
        el: '#skill-delete-form',
        beforeMount() {
            this.id = this.$el.querySelector('#Id').value;
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
    });
}
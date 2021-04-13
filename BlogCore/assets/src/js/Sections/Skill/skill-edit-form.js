if (document.getElementById('skill-edit-form') !== null) {
    new Vue({
        el: '#skill-edit-form',
        beforeMount() {
            this.id = this.$el.querySelector('#Id').value;
            this.name = this.$el.querySelector('#Name').value;
            this.proficiency = this.$el.querySelector('#Proficiency').value;
        },
        data: function () {
            return {
                errors: {
                    name: null,
                    proficiency: null,
                    result: null
                },
                id:'',
                name: '',
                proficiency: 'Basic',
                url: '/api/Skill'
            };
        },
        methods: {
            checkForm() {
                const name = this.validateName();
                const proficiency = this.validateProficiency();

                return name && proficiency;
            },
            submit() {
                common.put(`${this.url}(${this.id})`, { Id:this.id, Name: this.name, Proficiency: this.proficiency })
                    .then(() => window.location.href = "/Skill/Index")
                    .catch(response => this.errors.result = response.error.message);
            },
            validateName() {
                if (this.name.length > 0) {
                    this.errors.name = null;
                    return true;
                }
                this.errors.name = 'Skill name is required.';
                return false;
            },
            validateProficiency() {
                if (this.proficiency.length > 0) {
                    this.errors.proficiency = null;
                    return true;
                }
                this.errors.proficiency = 'Proficiency level is required.';
                return false;
            }
        },
        watch: {
            name: 'validateName',
            proficiency: 'validateProficiency'
        }
    });
}
if (document.getElementById('job-edit-form') !== null) {
    new Vue({
        el: '#job-edit-form',
        beforeMount() {
            let JobEnd = this.$el.querySelector('#JobEnd').value;
            
            this.company = this.$el.querySelector('#Company').value;
            this.description = this.$el.querySelector('#Description').value;
            this.id = this.$el.querySelector('#Id').value;
            this.jobEnd = null;
            this.jobStart = new Date(Date.parse(this.$el.querySelector('#JobStart').value)) || null;
            this.title = this.$el.querySelector('#Title').value;

            if (JobEnd.length > 0) {
                this.jobEnd = new Date(Date.parse(this.$el.querySelector('#JobEnd').value)) || null;
            }

        },
        components: {
            vuejsDatepicker: window.vuejsDatepicker
        },
        data: function () {
            return {
                company: '',
                description: '',
                dateFormat: 'MMMM yyyy',
                errors: {
                    company: null,
                    description:null,
                    jobEnd: null,
                    jobStart: null,
                    result: null,
                    title: null
                },
                id: '',
                jobEnd: '',
                jobStart:'',
                title: '',
                url: '/api/Job'
            };
        },
        methods: {
            checkForm() {
                const company = this.validateCompany();
                const description = this.validateDescription();
                const jobEnd = this.validateJobEnd();
                const jobStart = this.validateJobStart();
                const title = this.validateTitle();
                return company && description && jobEnd && jobStart && title;
            },
            submit() {
                if (this.checkForm()) {
                    common.put(`${this.url}(${this.id})`, { Company: this.company, Description: this.description, Id: this.id, JobEnd: this.jobEnd, JobStart: this.jobStart, Title: this.title })
                        .then(() => window.location.href = "/Job/Index")
                        .catch(response => console.error(response.error.message));
                }
            },
            validateCompany() {
                if (this.company.length > 0) {
                    this.errors.company = null;
                    return true;
                }
                this.errors.company = 'Company name is required.';
                return false;
            },
            validateDescription() {
                if (this.description.length > 0) {
                    this.errors.description = null;
                    return true;
                }
                this.errors.description = 'Job description is required.';
                return false;

            },
            validateJobEnd() {
                if (this.jobStart && this.jobEnd && !this.validateDateMatch()) {
                    this.errors.jobEnd = 'End date cannot be before start date.';
                    return false;
                }
                this.errors.jobEnd = null;
                return true;
            },
            validateJobStart() {
                if (this.jobStart && this.jobEnd && !this.validateDateMatch()) {
                    this.errors.jobStart = 'Start date cannot be after end date.';
                    return false;
                }
                else if (!this.jobStart) {
                    this.errors.jobStart = 'Start date is required.';
                    return false;
                }

                this.errors.jobStart = null;
                return true;
            },
            validateTitle() {
                if (this.title.length > 0) {
                    this.errors.title = null;
                    return true;
                }
                this.errors.title = "Title is required";
                return false;
            },
            validateDateMatch() {
                if (this.jobEnd < this.jobStart) {
                    return false;
                }
                return true;
            },
            validateDates() {
                this.validateJobEnd();
                this.validateJobStart();
            }
        },
        watch: {
            company: 'validateCompany',
            description: 'validateDescription',
            jobEnd: 'validateDates',
            jobStart: 'validateDates',
            title: 'validateTitle'
        }
    });
}
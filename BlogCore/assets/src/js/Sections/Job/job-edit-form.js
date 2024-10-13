if (document.getElementById('job-edit-form') !== null) {
    Promise.all([
        import('vue'),
        import('@vuepic/vue-datepicker'),
        import('common'),
        import('date-fns')
    ]).then(([{ createApp }, { default: VueDatePicker }, { common }, { parse }]) => {
        createApp({
            mounted() {
                let JobEnd = document.getElementById('JobEnd')._value;
                let JobStart = document.getElementById('JobStart')._value;
                this.company = document.getElementById('Company')._value;
                this.description = document.getElementById('Description').textContent;
                this.id = document.getElementById('Id')._value;
                this.jobEnd = null;
                this.jobStart = parse(JobStart, 'dd/MM/yyyy', new Date()) || null;
                this.title = document.getElementById('Title')._value;

                if (JobEnd.length > 0) {
                    this.jobEnd = parse(JobEnd, 'dd/MM/yyyy', new Date()) || null;
                }

            },
            components: {
                VueDatePicker
            },
            data: function () {
                return {
                    company: '',
                    description: '',
                    dateFormat: 'MMMM yyyy',
                    errors: {
                        company: null,
                        description: null,
                        jobEnd: null,
                        jobStart: null,
                        result: null,
                        title: null
                    },
                    id: '',
                    jobEnd: '',
                    jobStart: '',
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
        }).mount('#job-edit-form');
    }).catch(error => {
        console.error("Failed to load module: ", error);
    })
}

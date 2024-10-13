if (document.getElementById('contact-section') !== null) {
    import('vue').then(({ createApp }) => {
        createApp({
            data: function () {
                return {
                    email: '',
                    errors: {
                        email: null,
                        name: null,
                        message: null,
                        token: null
                    },
                    loading: false,
                    name: '',
                    message: '',
                    token: '',
                    url: '/api/Email'
                };
            },
            methods: {
                checkForm() {
                    const email = this.validateEmail();
                    const name = this.validateName();
                    const message = this.validateMessage();
                    const token = this.validateToken();

                    return email && name && message && token;
                },
                clearErrors() {
                    this.errors.email = null;
                    this.errors.name = null;
                    this.errors.message = null;
                },
                resetForm() {
                    this.email = '';
                    this.name = '';
                    this.message = '';
                    this.token = '';
                    grecaptcha.enterprise.reset();
                },
                submit() {
                    if (this.checkForm()) {
                        this.loading = true;
                        common.post(this.url, { Email: this.email, Name: this.name, Message: this.message, Token: this.token })
                            .then(response => {
                                this.resetForm();
                                this.clearErrors();
                                Vue.$toast.success("Message Sent!");
                            })
                            .catch(response => console.error(response))
                            .finally(() => this.loading = false);
                    }
                },
                validateEmail() {
                    if (this.email.length > 0) {
                        this.errors.email = null;
                        return true;
                    }
                    this.errors.email = "Email is required.";
                    return false;
                },
                validateName() {
                    if (this.name.length > 0) {
                        this.errors.name = null;
                        return true;
                    }
                    this.errors.name = "Name is required.";
                    return false;
                },
                validateMessage() {
                    if (this.message.length > 0) {
                        this.errors.message = null;
                        return true;
                    }

                    this.errors.message = "Message is required.";
                    return false;
                },
                validateToken() {
                    this.token = document.getElementById('g-recaptcha-response').value;
                    if (this.token.length > 0) {
                        this.errors.token = null;
                        return true;
                    }

                    this.errors.token = "Please complete the ReCaptcha.";
                    return false;

                }
            }
        }).mount("#contact-section");

        //ReScale the recaptcha for smaller devices.
        $(function () {
            function rescaleCaptcha() {
                var width = $('.g-recaptcha').parent().width();
                var scale;
                if (width < 302) {
                    scale = width / 302;
                } else {
                    scale = 1.0;
                }

                $('.g-recaptcha').css('transform', 'scale(' + scale + ')');
                $('.g-recaptcha').css('-webkit-transform', 'scale(' + scale + ')');
                $('.g-recaptcha').css('transform-origin', '0 0');
                $('.g-recaptcha').css('-webkit-transform-origin', '0 0');
            }

            rescaleCaptcha();
            $(window).resize(function () { rescaleCaptcha(); });

        });
    }).catch(error => {
        console.error("Failed to load module: ", error);
    })
}

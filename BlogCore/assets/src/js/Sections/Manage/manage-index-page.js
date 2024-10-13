if (document.getElementById('manage-index-page') !== null) {
    Promise.all([
        import("vue"),
        import("components"),
        import("common")
    ]).then(([{ createApp }, { galleryPickerComponent }, { common }]) => {
        createApp({
            mounted() {
                common.get(this.endpoint)
                    .then(response => this.source = response.value)
                    .catch(error => console.error(error));
                this.bio = document.getElementById('Account_Bio')._value;
                this.contactImage = document.getElementById('Configuration_ContactImage')._value;
                this.cvLink = document.getElementById('Configuration_CVLink')._value;
                this.email = document.getElementById('Account_Email')._value;
                this.firstName = document.getElementById('Account_FirstName')._value;
                this.githubLink = document.getElementById('Configuration_GithubLink')._value;
                this.landingImage = document.getElementById('Configuration_LandingImage')._value;
                this.lastName = document.getElementById('Account_LastName')._value;
                this.linkedInLink = document.getElementById('Configuration_LinkedInLink')._value;
                this.phoneNumberValue = document.getElementById('Account_PhoneNumber')._value.replace(this.numberRegex, '').split('');
                this.profile = document.getElementById('Account_Link')._value;
                this.skillsImage = document.getElementById('Configuration_SkillImage')._value;
                this.title = document.getElementById('Account_Title')._value;
            },
            components: {
                galleryPickerComponent
            },
            data: function () {
                return {
                    bio: '',
                    contactImage: '',
                    cvLink: '',
                    email: '',
                    endpoint: '/api/MediaLink?$expand=Group',
                    firstName: '',
                    githubLink: '',
                    landingImage: '',
                    lastName: '',
                    linkedInLink: '',
                    phoneNumber: '(###) ###-####',
                    phoneNumberValue: [],
                    profile: '',
                    skillsImage: '',
                    source: [],
                    title: '',
                    numberRegex: /[^0-9]/g,
                    userInfoUrl: '/api/Manage/PostUserInfo',
                    configUrl: '/api/Manage/PostConfiguration',
                    passwordChangeUrl: '/api/Manage/PostPasswordChange',
                    loadingUserInfo: false,
                    loadingPasswordChange: false,
                    loadingSiteConfig: false,
                    oldPassword: '',
                    newPassword: '',
                    confirmPassword: ''
                };
            },
            methods: {
                getProfilePic() {
                    return this.profile || '/assets/dist/images/default_profile_pic.png';
                },
                submitUserInfo() {
                    this.loadingUserInfo = true;
                    let phoneNumber = this.phoneNumberValue.length > 0 ? this.phoneNumber : '';
                    common.post(this.userInfoUrl, { Email: this.email, FirstName: this.firstName, LastName: this.lastName, Link: this.profile, Title: this.title, PhoneNumber: phoneNumber, Bio: this.bio })
                        .then(response => console.log(response))
                        .catch(response => console.error(response))
                        .finally(() => this.loadingUserInfo = false);
                },
                submitPasswordChange() {
                    this.loadingPasswordChange = true;
                    common.post(this.passwordChangeUrl, { OldPassword: this.oldPassword, NewPassword: this.newPassword })
                        .then(response => {
                            console.log(response);
                            this.clearPasswordForm();
                        })
                        .catch(response => console.error(response))
                        .finally(() => this.loadingPasswordChange = false);
                },
                submitSiteConfiguration() {
                    this.loadingSiteConfig = true;
                    common.post(this.configUrl, { LandingImage: this.landingImage, SkillImage: this.skillsImage, ContactImage: this.contactImage, GithubLink: this.githubLink, LinkedInLink: this.linkedInLink, CVLink: this.cvLink })
                        .then(response => console.log(response))
                        .catch(response => console.error(response))
                        .finally(() => this.loadingSiteConfig = false);
                },
                format(e) {
                    if (e.keyCode === 8) {
                        this.phoneNumberValue.pop();
                    }
                    else {
                        if (parseInt(e.key) >= 0 && this.phoneNumberValue.length < 10) {
                            this.phoneNumberValue.push(e.key);
                        }
                    }

                    this.phoneNumberValue = [...this.phoneNumberValue];
                },
                clearPasswordForm() {
                    this.newPassword = '';
                    this.oldPassword = '';
                    this.confirmPassword = '';
                }
            },
            watch: {
                phoneNumberValue: function () {
                    this.phoneNumber = `(${this.phoneNumberValue[0] || '#'}${this.phoneNumberValue[1] || '#'}${this.phoneNumberValue[2] || '#'}) ${this.phoneNumberValue[3] || '#'}${this.phoneNumberValue[4] || '#'}${this.phoneNumberValue[5] || '#'}-${this.phoneNumberValue[6] || '#'}${this.phoneNumberValue[7] || '#'}${this.phoneNumberValue[8] || '#'}${this.phoneNumberValue[9] || '#'}`;
                }
            }
        }).mount("#manage-index-page");
    }).catch(error => {
        console.error("Failed to load module: ", error);
    })
}

if (document.getElementById('manage-index-page') !== null) {
    new Vue({
        el: "#manage-index-page",
        beforeMount() {
            common.get(this.endpoint)
                .then(response => this.source = response.value)
                .catch(error => console.error(error));

            this.bio = this.$el.querySelector('#Account_Bio').value;
            this.contactImage = this.$el.querySelector('#Configuration_ContactImage').value;
            this.email = this.$el.querySelector('#Account_Email').value;
            this.firstName = this.$el.querySelector('#Account_FirstName').value;
            this.githubLink = this.$el.querySelector('#Configuration_GithubLink').value;
            this.landingImage = this.$el.querySelector('#Configuration_LandingImage').value;
            this.lastName = this.$el.querySelector('#Account_LastName').value;
            this.linkedInLink = this.$el.querySelector('#Configuration_LinkedInLink').value;
            this.phoneNumberValue = this.$el.querySelector('#Account_PhoneNumber').value.replace(this.numberRegex,'').split('');
            this.profile = this.$el.querySelector('#Account_Link').value;
            this.skillsImage = this.$el.querySelector('#Configuration_SkillImage').value;
            this.title = this.$el.querySelector('#Account_Title').value;
        },
        components: {
            galleryPickerComponent
        },
        data: function () {
            return {
                bio:'',
                contactImage: '',
                email:'',
                endpoint: '/api/MediaLink?$expand=Group',
                firstName: '',
                githubLink:'',
                landingImage: '',
                lastName: '',
                linkedInLink:'',
                phoneNumber: '(###) ###-####',
                phoneNumberValue:[],
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
                confirmPassword:''
            };
        },
        methods: {
            getProfilePic() {
                return this.profile || '/assets/dist/images/default_profile_pic.png';
            },
            submitUserInfo() {
                this.loadingUserInfo = true;
                let phoneNumber = this.phoneNumberValue.length > 0 ? this.phoneNumber : '';
                common.post(this.userInfoUrl, { Email:this.email, FirstName: this.firstName, LastName: this.lastName, Link: this.profile, Title: this.title, PhoneNumber: phoneNumber, Bio: this.bio })
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
                common.post(this.configUrl, { LandingImage: this.landingImage, SkillImage: this.skillsImage, ContactImage: this.contactImage, GithubLink: this.githubLink, LinkedInLink: this.linkedInLink })
                    .then(response=>console.log(response))
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
    });
}
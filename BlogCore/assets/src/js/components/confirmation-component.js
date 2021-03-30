const confirmationComponent = Vue.component('confirmation-component', {
    methods: {
        cancel() {
            this.$emit('cancel');
        },
        confirm() {
            this.$emit('confirm');
        }
    },
    props: {
        header: {
            required: true,
            type: String
        },
        body: {
            required: true,
            type:String
        }
    },
    template:
        `
            <transition name="modal-fade">
                <div class="overlay">
                    <div class="modal-component">
                        <div class="header">
                            <i class="fas fa-info-circle fa-fw fa-2x"></i>
                        </div>
                        <div class="body">
                            <h3>{{header}}</h3>
                            <p>
                                {{body}}
                            </p>
                        </div>
                        <div class="footer">
                            <button class="btn btn-outline-primary" v-on:click="confirm">Confirm</button>
                            <button class="btn btn-outline-danger" v-on:click="cancel">Cancel</button>
                        </div>
                    </div>  
                </div>
            </transition>
        `
});
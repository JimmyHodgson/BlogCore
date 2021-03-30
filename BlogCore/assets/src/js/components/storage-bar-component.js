const storageBarComponent = Vue.component('storage-bar-component', {
    methods: {
        format(value) {
            const length = String(value).length;
            if (length >= 10) {
                return `${this.formatNumber(value / Math.pow(1024,3))} GB`;
            }
            else if (length >= 7) {
                return `${this.formatNumber(value / Math.pow(1024, 2))} MB`;
            }
            else {
                return `${this.formatNumber(value / 1024)} KB`;
            }
        },
        formatNumber(value) {
            return value.toLocaleString('en-US',
                {
                    minimumFractionDigits: 0,
                    maximumFractionDigits: 1
                });
        },
        getClass() {
            let percentage = this.getPercentage();
            if (percentage > 75) {
                return "-warning";
            } else if (percentage > 45) {
                return "-ok";
            }
            else {
                return "-good";
            }
        },
        getPercentage() {
            return (this.used / this.max * 100) || 0;
        },
        getUsedString() {
            return `${this.used / 100 * this.max.value}${this.unit}`;
        }
    },
    props: {
        max: {
            required: true,
            type: Number
        },
        used: {
            required: true,
            type: Number
        }
    },
    template:
        `   <div class="storage-bar-container">
                <div class="values">
                    <div class="item monofont">
                        {{format(used)}}
                    </div>
                    <div class="item -right monofont">
                        {{format(max)}}
                    </div>
                </div>
                <div class="storage-bar">
                    <div class="used" v-bind:style="{width:getPercentage()+'%'}"  v-bind:class="getClass()"></div>
                    <div class="available"></div>
                </div>
                <div class="info monofont">
                    {{formatNumber(getPercentage())+'%'}} of storage used
                </div>
            </div>
        `
});
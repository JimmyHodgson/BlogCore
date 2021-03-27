let tableComponent = Vue.component('table-component', {
    data: function () {
        return {
            count: 0,
            data: [],
            page: 0,
            pageSize: 10,
            headers: [],
            performance: "0 ms",
            success: false
        };
    },
    computed: {
        totalPages: function () {
            return parseInt(Math.ceil(this.count / this.pageSize));
        },
        hasNextPage: function () {
            return this.page < this.totalPages;
        },
        hasPrevPage: function () {
            return this.page > 1;
        }
    },
    methods: {
        nextPage() {

        },
        prevPage() {

        },
        format(data) {
            //let res = [];
            //for (row in data) {
            //    let current = [];
            //    for (key in row) {
            //        if (format[key]) {
            //            switch (format[key].type) {
            //                case 'url':
            //                    current.push(`<img src="${row[key]}"/>`);
            //                    break;
            //                default:
            //                    current.push(row[key]);
            //                    break;
            //            }
            //        } else {
            //            current.push(row[key]);
            //        }
            //    }
            //    res.push(current);
            //}
            //return res;
            return data;
        },
        getHeaders() {
            let headers = [];
            if (this.data.length > 0) {
                for (let key in this.data[0]) {
                    headers.push(key);
                }
            }
            this.headers = headers;
        }
    },
    mounted: function () {
        let start = performance.now();
        let end;
        common.get(`${this.url}?$count=true&$top=${this.pageSize}`)
            .then(data => {
                end = performance.now();
                this.performance = `${(end - start).toFixed(0)} ms`;
                console.log(data);
                this.count = data['@odata.count'];
                if (data.value.length > 0) {
                    this.page = 1;
                    this.data = this.format(data.value);
                }
                this.getHeaders();
                this.success = true;
            })
            .catch(error => {
                console.error(error);
            });
    },
    props: {
        details: {
            required: false,
            type: String
        },
        schema: {
            required: true,
            type: Array
        },
        remove: {
            required: false,
            type: String
        },
        ribbon: {
            required: false,
            type: Array
        },
        update: {
            required: false,
            type: String
        },
        url: {
            required: true,
            type: String
        }
    },
    template:
        `
            <div v-if="success">
                <div class="table-ribbon">
                    <a v-for="action in ribbon" v-bind:href="action.url" class="item">
                        <i class="fa-fw" v-bind:class="action.icon"></i>
                    </a>
                </div>
                <div class="table-responsive">
                    <table class="table bc-table">
                        <thead class="header">
                            <tr>
                                <th v-for="header in headers">
                                    {{header}}
                                </th>
                                <th v-if="update"></th>
                                <th v-if="details"></th>
                                <th v-if="remove"></th>
                            </tr>
                        </thead>
                        <tbody class="body">
                            <tr v-for="row in data">
                                <td v-for="(value,name) in row">{{value}}</td>
                                <td v-if="update" class="button">
                                    <a href="">
                                        <i class="fas fa-edit fa-fw"></i>
                                    </a>
                                </td>
                                <td v-if="details" class="button">
                                    <a href="">
                                        <i class="fas fa-file-alt fa-fw"></i>
                                    </a>
                                </td>
                                <td v-if="remove" class="button">
                                    <a href="">
                                        <i class="fas fa-trash-alt fa-fw"></i>
                                    </a>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div v-if="data.length===0" class="empty-panel">
                    <div class="header">
                        <i class="far fa-info-circle fa-fw fa-2x"></i>
                    </div>
                    <div class="body">
                        There is no data to show.
                    </div>
                </div>
                <div class="table-footer">
                    <div>
                        <i class="far fa-clock fa-fw item"></i> {{performance}}
                    </div>
                    <div>
                        <i class="far fa-chevron-left fa-fw item button" v-on:click="prevPage" v-bind:class="hasPrevPage?'clickable':'-disabled'"></i>
                        {{page}}
                        <i class="far fa-chevron-right fa-fw item button" v-on:click="nextPage" v-bind:class="hasNextPage?'clickable':'-disabled'"></i>
                    </div>
                </div>
            </div>
            <div v-else class="loading-panel">
                <i class="fas fa-sync fa-spin fa-fw fa-2x"></i>
            </div>

        `
});

window.tableComponent = tableComponent;
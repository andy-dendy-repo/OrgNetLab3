var app = new Vue({
    el: '#app',
    data: {
        current: {},
        /*---users---*/
        schedule: [],
    },
    created: function () {
        this.refresh();
    },
    methods: {
        refresh: async function () {
            await this.getCurrent();
        },
        getCurrent: async function () {
            this.current = await Get('/api/account/current');
            this.schedule = await Get('/api/user/student/schedule');
        },
    }
});
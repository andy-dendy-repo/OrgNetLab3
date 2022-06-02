var app = new Vue({
    el: '#app',
    data: {
        current: {},
        /*---users---*/
        users: [],
        user: {},
        /*---groups---*/
        groups: [],
        group: {},
        /*---subjects---*/
        subjects: [],
        subject: {},
        /*---studentGroups---*/
        studentGroups: [],
        studentGroup: {},
        /*---lessons---*/
        lessons: [],
        lesson: {},
    },
    created: function () {
        this.refresh();
    },
    methods: {
        refresh: async function () {
            await this.getCurrent();
            await this.getUsers();
            await this.getGroups();
            await this.getSubjects();
            await this.getStudentGroups();
            await this.getLessons();
        },
        getCurrent: async function() {
            this.current = await Get('/api/account/current');
        },
        /*---users---*/
        getUsers: async function () {
            this.users = (await Get('/api/user')).map(x => { x.dateOfBirth = x.dateOfBirth.split('T')[0]; return x; });
        },
        addUser: async function () {
            await Post('/api/user/register', this.user);
            await this.refresh();
        },
        updateUser: async function () {
            await Put('/api/user/', this.user);
            await this.refresh();
        },
        deleteUser: async function (user) {
            await Delete('/api/user' + user.id);
            await this.refresh();
        },
        /*---groups---*/
        getGroups: async function () {
            this.groups = await Get('/api/group/');
        },
        addGroup: async function () {
            await Post('/api/group', this.group);
            await this.refresh();
        },
        updateGroup: async function () {
            await Put('/api/group/', this.group);
            await this.refresh();
        },
        deleteGroup: async function (group) {
            await Delete('/api/group' + group.id);
            await this.refresh();
        },
        /*---subjects---*/
        getSubjects: async function () {
            this.subjects = await Get('/api/subject/');
        },
        addSubject: async function () {
            await Post('/api/subject', this.subject);
            await this.refresh();
        },
        updateSubject: async function () {
            await Put('/api/subject/', this.subject);
            await this.refresh();
        },
        deleteSubject: async function (subject) {
            await Delete('/api/subject' + subject.id);
            await this.refresh();
        },
        /*---studentGroups---*/
        getStudentGroups: async function () {
            let arr = await Get('/api/StudentGroup/');
            this.StudentGroupGroupping(arr);
        },
        addStudentGroup: async function () {
            await Post('/api/StudentGroup', this.studentGroup);
            await this.refresh();
        },
        deleteStudentGroup: async function (studentGroup) {
            await Delete('/api/StudentGroup/' + studentGroup.groupId + '/' + studentGroup.studentId);
            await this.refresh();
        },
        /*---lessons---*/
        getLessons: async function () {
            this.lessons = await Get('/api/lesson/');
        },
        addLesson: async function () {
            await Post('/api/lesson', this.lesson);
            await this.refresh();
        },
        updateLesson: async function () {
            await Put('/api/lesson/', this.lesson);
            await this.refresh();
        },
        deleteLesson: async function (lesson) {
            await Delete('/api/lesson' + lesson.id);
            await this.refresh();
        },
        /*---utils---*/
        getUserById: function (id) {
            return this.users.filter(x => x.id == id)[0];
        },
        getGroupById: function (id) {
            return this.groups.filter(x => x.id == id)[0];
        },
        StudentGroupGroupping: function (arr) {
            let groups = {};
            for (var i = 0; i < arr.length; i++) {
                var groupName = arr[i].groupId;
                if (!groups[groupName]) {
                    groups[groupName] = [];
                }
                groups[groupName].push(arr[i].studentId);
            }
            this.studentGroups = [];
            for (var groupName in groups) {
                this.studentGroups.push({ groupId: groupName, students: groups[groupName] });
            }
        }
    }
});
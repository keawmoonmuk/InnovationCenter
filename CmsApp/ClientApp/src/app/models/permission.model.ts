
//************PermissionNames*********************

export type PermissionNames =
    'View Users' | 'Manage Users' |
    'View Roles' | 'Manage Roles' | 'Assign Roles';

export type PermissionValues =
    'users.view' | 'users.manage' |
    'roles.view' | 'roles.manage' | 'roles.assign';

export class Permission {

    public static readonly viewUsersPermission: PermissionValues = 'users.view';      // แสดงสิทธิ์ user
    public static readonly manageUsersPermission: PermissionValues = 'users.manage';  // จัดการสิทธิ์ user
    public static readonly viewRolesPermission: PermissionValues = 'roles.view';      // แสดงบทบาทสิทธิ์
    public static readonly manageRolesPermission: PermissionValues = 'roles.manage';  // จัดการบทบาทสิทธิ์
    public static readonly assignRolesPermission: PermissionValues = 'roles.assign';  // กำหนดบทบาทสิทธิ์

    constructor(name?: PermissionNames, value?: PermissionValues, groupName?: string, description?: string) {
        this.name = name;
        this.value = value;
        this.groupName = groupName;
        this.description = description;
    }

    public name: PermissionNames;
    public value: PermissionValues;
    public groupName: string;
    public description: string;
}

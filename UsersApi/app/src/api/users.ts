import config, { HttpModule } from "src/config";

export type UserListResponse = {
    total: number;
    users: UserDto[];
};

export type UserDto = {
    id: string;
    login: string;
    name: string;
    defaultSalary: number;
    position: string;
}


export class PassesApiService {
    private _baseUrl: string;
    constructor() {
        this._baseUrl = config.apiUrl[HttpModule.Users];
    }

    list = async (): Promise<UserListResponse> => {
        const response = await fetch(this._baseUrl + '/list');
        return response.json();
    };
}
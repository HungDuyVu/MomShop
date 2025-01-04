import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { LoginDto } from "src/models/login";
import { RegisterDto } from "src/models/register";
import { UserDto } from "src/models/user";

@Injectable({
    providedIn: 'root',
})
export class UserService {
    baseUrl = 'http://localhost:5001/api/user/';
    constructor(private http: HttpClient) { }
    login(input: LoginDto) {
        return this.http.post(this.baseUrl + 'login', input)
    }
    register(input: RegisterDto) {
        return this.http.post(this.baseUrl + 'register', input)
    }

    //quan code tam
    findUser() {
        const user = JSON.parse(localStorage.getItem('user'));
        return this.http.get(this.baseUrl + 'find/' + user.id);
        // return this.http.post(this.baseUrl+'register',input,{responseType: 'text'})
    }
    updateInforUser(user:UserDto){
        return this.http.put(this.baseUrl +'update',user,{responseType: 'text'});
    }
    findUserById(id){
        return this.http.get(this.baseUrl + 'find/'+ id);
    }
    getAllCollection(){
        return this.http.get(this.baseUrl + 'collection')
    }
    getDetailCollection(id){
        return this.http.get(this.baseUrl+'collection/detail/'+id);
    }
}
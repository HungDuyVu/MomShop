import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FeedbackDto } from 'src/models/user/FeedbackDto';


@Injectable({
    providedIn: 'root'
})
export class UserFeedBackService {
    baseUrl = 'http://localhost:5001/api/user/feedback/';
    constructor(private http: HttpClient) { }

    addFeedbackUser(input: FeedbackDto) {
        return this.http.post(this.baseUrl + 'add', input)
    }
}
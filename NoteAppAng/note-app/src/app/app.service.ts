import {inject, Injectable} from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from "@angular/common/http";
import {Note} from "./models/note";
import {User} from "./models/user";
import {environment} from "./enviroment";
import {J} from "@angular/cdk/keycodes";
import {Router} from "@angular/router";

@Injectable({
  providedIn: 'root'
})
export class AppService {

  url: string = environment.BACKEND_API_URL;
  session: User | undefined;
  constructor(private  http: HttpClient = inject(HttpClient), private router: Router) {
    let session: any = localStorage.getItem('session');
    if (session) {
      session = JSON.parse(session)
    }
    this.session = session as User
  }

  getAllNotes(userId: any){
    let params = new HttpParams().set('userId', userId);
    return this.http.get<Note[]>('https://localhost:7131/api/Users/allNotes', {params: params});
  }

  createNote(note: Note){
    return this.http.post<Note>('https://localhost:7131/api/Notes', note);
  }

  editNote(id:number,note: Note){
    return this.http.put<Note>(`https://localhost:7131/api/Notes/${id}`, note);
  }

  deleteNote(id: number){
    return this.http.delete<Note>(`https://localhost:7131/api/Notes/${id}`);
  }

   public createUser(data : any) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Access-Control-Allow-Origin': '*'
      })
    };
     return  this.http.post(this.url + 'Users', data, httpOptions);
  }

  loginUser(user: User){
    return this.http.post<User>('https://localhost:7131/api/Users/login', user);
  }
  public makeLoginSession(loginUser: User) {
    this.session = loginUser
    localStorage.setItem('session', JSON.stringify(this.session))
  }
  public logout() {
    this.session = undefined;
    localStorage.removeItem('session');
    this.router.navigate([""]);
  }

}

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { from } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.sass']
})
export class LoginComponent implements OnInit {

  invalidLogin!: boolean;

  constructor(private _router: Router, private _http: HttpClient) { }

  ngOnInit(): void {
  }

  login(form: NgForm) {
    const credentials = {
      'username': form.value.username,
      'password': form.value.password,
    }

    const headers = new HttpHeaders().set(
      'Content-Type',
      'application/json'
    );

    this._http.post('/api/v1/auth/login', credentials, { headers })
      .subscribe(res => {
        const token = (<any>res).token;
        localStorage.setItem("jwt", token);
        this.invalidLogin = false;
        this._router.navigate(['/']);
      }, err => {
        this.invalidLogin = true;
      })
  }

}

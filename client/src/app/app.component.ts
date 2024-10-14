import { HttpClient, HttpClientModule, HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { error } from 'console';
import { response } from 'express';
import { bootstrapApplication } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  imports: [HttpClientModule , CommonModule], // Import HttpClientModule here
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{
  
  title = 'The Dating App';
  users : any;
  
  constructor(private http:HttpClient) {}

  ngOnInit(){
    this.getUsers();
  }

  getUsers() {
    this.http.get('https://localhost:7188/api/users').subscribe(
      (response) => {
        this.users = response;
        //console.log(this.users);
      },
      (error) => {
        console.log(error);
      }
    );
  }
  
  
}
import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  leftPlayerBoard: any;
  table2: any;
  constructor(private http: HttpClient) { }
 

  ngOnInit(): void {
  }

  setNewGame(){
    this.getBoard1();
    this.getBoard2();
  }

  getBoard1(){
    this.http.get('https://localhost:5001/api/tables/table1').subscribe(response =>{
      this.leftPlayerBoard = response;
    }, error =>{
      console.log(error);
    });
  }

  getBoard2(){
    this.http.get('https://localhost:5001/api/tables/table2').subscribe(response => {
      this.table2 = response;
    }, error => {
      console.log(error);
    });
  }

}

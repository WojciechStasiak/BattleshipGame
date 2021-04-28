import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Battleship Game';
  leftPlayerBoard: any;
  table2: any;
  setInterval1 = null;
  left: boolean = false;
  winner: any;

  constructor(private http: HttpClient) { }
  ngOnInit(): void {
    this.getBoard1();
    this.getBoard2();
  }

  setUpNewGame() {
    this.getBoard1();
    this.getBoard2();
    this.winner = null;
  }
  getBoard1() {
    this.http.get('https://localhost:5001/api/tables/table1').subscribe(response => {
      this.leftPlayerBoard = response;
    }, error => {
      console.log(error);
    });
  }

  getBoard2() {
    this.http.get('https://localhost:5001/api/tables/table2').subscribe(response => {
      this.table2 = response;
    }, error => {
      console.log(error);
    });
  }

  shotAtLeftPlayer() {
    this.http.get('https://localhost:5001/api/tables/shotAtLeftPlayer').subscribe(response => {
      this.leftPlayerBoard = response;
    }, error => {
      console.log(error);
    });
  }

  shotAtRigtPlayer() {
    this.http.get('https://localhost:5001/api/tables/shotAtRightPlayer').subscribe(response => {
      this.table2 = response;
    }, error => {
      console.log(error);
    });
  }

  giveWinnerOfTheGame() {
    this.http.get('https://localhost:5001/api/tables/giveWinner').subscribe(response => {
      this.winner = response;
    }, error => {
      console.log(error);
    });
  }
  setIntrvl() {
    this.clearInterval();
    this.setInterval1 = setInterval(() => this.oneStep(), 100);
    //this.getBoard1();
    //this.getBoard2();
  }
  oneStep() {
    if (this.left == false) {
      this.shotAtRigtPlayer();
      this.left = !this.left;
      this.giveWinnerOfTheGame();
    }
    else {
      this.shotAtLeftPlayer();
      this.left = !this.left;
      this.giveWinnerOfTheGame();
    }
  }

  clearInterval() {
    clearInterval(this.setInterval1);
  }
}

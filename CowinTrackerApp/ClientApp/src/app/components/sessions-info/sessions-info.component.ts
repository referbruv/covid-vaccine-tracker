import { Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-sessions-info',
  templateUrl: './sessions-info.component.html',
  styleUrls: ['./sessions-info.component.css']
})
export class SessionsInfoComponent implements OnInit {

  @Input('sessions') sessions: any;
  @Input('startDate') startDate: any;

  constructor(public m: NgbActiveModal) { }

  ngOnInit() {
  }

  showDate(dateString: string) {
    return dateString;
    // let parts = dateString.split("-");
    // return (new Date(dateString)).toLocaleDateString();
  }

}

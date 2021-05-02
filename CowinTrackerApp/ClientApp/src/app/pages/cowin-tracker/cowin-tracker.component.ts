import { Component, Inject, OnInit } from '@angular/core';
import { NgbCalendar, NgbDateParserFormatter, NgbDateStruct, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CowinService } from '../../cowin.service';
import { CustomDateParserFormatter } from '../../customdateformatter';
import { SessionsInfoComponent } from '../../components/sessions-info/sessions-info.component';

@Component({
  selector: 'app-cowin-tracker',
  templateUrl: './cowin-tracker.component.html',
  styleUrls: ['./cowin-tracker.component.css'],
  providers: [
    { provide: NgbDateParserFormatter, useClass: CustomDateParserFormatter }
  ]
})
export class CowinTrackerComponent implements OnInit {

  private apiBasePath = 'api/cowindata';

  states: State[];
  districts: District[];
  selectedState: number;
  selectedDistrict: number;
  baseUrl: string;
  selectedDate: NgbDateStruct = this.ngbCalendar.getToday();
  data: any;

  constructor(
    private client: CowinService,
    @Inject('BASE_URL') baseUrl: string,
    private ngbCalendar: NgbCalendar,
    private ngbModal: NgbModal) {
    this.baseUrl = baseUrl;
  }

  async ngOnInit() {
    this.states = await this.client.getData<State[]>(`${this.baseUrl}${this.apiBasePath}/states`);
    if (this.states.length > 0) {
      this.selectedState = this.states[0].state_id;
      await this.onStateChanged();
      await this.getDataForDistrictAndDate();
    }
  }

  async onStateChanged() {
    let state_id = this.selectedState;
    this.districts = await this.client.getData<District[]>(`${this.baseUrl}${this.apiBasePath}/states/${state_id}/districts/`);
    this.selectedDistrict = this.districts[0].district_id;
  }

  async onDistrictChanged() {
    console.log(this.selectedDistrict);
  }

  async onSelectedDateChanged(event) {
    this.selectedDate = event;
  }

  async getDataForDistrictAndDate() {
    let url = `${this.baseUrl}${this.apiBasePath}/sessions/${this.selectedDistrict}/startDate/${this.selectedDate.day}-${this.selectedDate.month}-${this.selectedDate.year}/`;
    // let url = `assets/download.json`;
    this.data = await this.client.getCowinData(url);
    console.log(this.data);
  }

  sessions: any[] = [];

  setSessionsContent(center: any) {
    let ref = this.ngbModal.open(SessionsInfoComponent, { size: 'lg' });
    ref.componentInstance.sessions = center.sessions;
    ref.componentInstance.startDate = `${this.selectedDate.day}-${this.selectedDate.month}-${this.selectedDate.year}`;
  }

  tConvert(time) {
    // Check correct time format and split into components
    time = time.toString().match(/^([01]\d|2[0-3])(:)([0-5]\d)(:[0-5]\d)?$/) || [time];

    if (time.length > 1) { // If time format correct
      time = time.slice(1);  // Remove full string match value
      time[3] = +time[0] < 12 ? 'AM' : 'PM'; // Set AM/PM
      time[0] = +time[0] % 12 || 12; // Adjust hours
    }
    return time.join(''); // return adjusted time or original string
  }
}

export interface State {
  state_id: number;
  state_name: string;
}

export interface District {
  district_id: number;
  district_name: string;
}

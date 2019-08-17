import { Component, OnInit, Input } from '@angular/core';

// Model
import { Country } from 'src/app/core/model/Geo/country.model';
import { TripFull } from 'src/app/core/model/Trip/trip-full.model';

@Component({
  selector: 'app-countries-list',
  templateUrl: './countries-list.component.html',
  styleUrls: ['./countries-list.component.scss']
})
export class CountriesListComponent implements OnInit {

  constructor() { }

  @Input() countriesForTrip: TripFull[];

  ngOnInit() {
  }

}

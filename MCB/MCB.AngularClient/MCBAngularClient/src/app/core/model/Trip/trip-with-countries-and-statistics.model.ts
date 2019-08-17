import { Trip } from './trip.model';
import { Statistics } from './trip-statistics.model';
import { Country } from '../Geo/country.model';

export class TripWithCountriesAndStatistics extends Trip {
  statistics: Statistics;
  countries: Country[];
}

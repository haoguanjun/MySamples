import { hero } from './classes/hero';
import { InMemoryDbService } from 'angular-in-memory-web-api';

export const HEROES: hero [] = [
    { id: 1, name: 'Armstrong'},
    { id: 2, name: 'Shane'},
    { id: 3, name: 'Daniel'},
    { id: 4, name: 'Travis'},
    { id: 5, name: 'Vicky'}
];

export class InMemoryDataService implements InMemoryDbService {
    createDb() {
      const heroes = [
        { id: 11, name: 'Mr. Nice' },
        { id: 12, name: 'Narco' },
        { id: 13, name: 'Bombasto' },
        { id: 14, name: 'Celeritas' },
        { id: 15, name: 'Magneta' },
        { id: 16, name: 'RubberMan' },
        { id: 17, name: 'Dynama' },
        { id: 18, name: 'Dr IQ' },
        { id: 19, name: 'Magma' },
        { id: 20, name: 'Tornado' }
      ];
      return {heroes};
    }
  }
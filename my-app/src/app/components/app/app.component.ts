import { Component } from '@angular/core';
import { HeroListComponent } from '../hero-list/hero-list.component';
import { MessagesComponent } from '../messages/messages.component';
import { DashboardComponent } from '../dashboard/dashboard.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'My First Angular Application';
}

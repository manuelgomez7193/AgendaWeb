import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})

export class NavbarComponent {
  constructor(public translateService: TranslateService) { 
    translateService.addLangs(['en', 'es', 'pt']);
    translateService.setDefaultLang('es');

    const browserLang = translateService.getBrowserLang();

    if(browserLang)
      translateService.use(browserLang.match(/en|es/) ? browserLang : 'es');
  }

  public languageMap: Record<string, string> = {
    'en': 'English',
    'es': 'Español',
    'pt': 'Português'
  };

  isMenuOpen: boolean = false;
  isDashboardOpen: boolean = false;

  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen;
  }

  toggleDashboard() {
    this.isDashboardOpen = !this.isDashboardOpen;
  }
  

}

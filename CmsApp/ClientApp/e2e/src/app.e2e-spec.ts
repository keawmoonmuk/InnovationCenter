// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

import { AppPage } from './app.po';

describe('QuickApp-STANDARD App', () => {
  let page: AppPage;

  beforeEach(() => {
    page = new AppPage();
  });

  it('should display application title: QuickApp Standard', () => {
    page.navigateTo();
    expect(page.getAppTitle()).toEqual('QuickApp Standard');
  });
});

import { Pars.Util.Puma.UI.AngularPage } from './app.po';

describe('pars.util.puma.ui.angular App', () => {
  let page: Pars.Util.Puma.UI.AngularPage;

  beforeEach(() => {
    page = new Pars.Util.Puma.UI.AngularPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ShopComponent } from './shop/shop.component';
import { ContactComponent } from './contact/contact.component';
import { ProductCardComponent } from './shop/product-card/product-card.component';
import { ProductInfoComponent } from './shop/product-info/product-info.component';
import { ErrorNotFoundComponent } from './core/error-not-found/error-not-found.component';
import { ErrorInternalServerComponent } from './core/error-internal-server/error-internal-server.component';

const routes: Routes = [

  // Strona Głóna
  {path: "", component:HomeComponent, data: {breadcrumb: "Strona Główna"}},
  // Sklep
  {path: "sklep", loadChildren: () => import('./shop/shop.module').then(t => t.ShopModule)},
  // Kontakt
  {path: "kontakt", component:ContactComponent},

  // Koszyk
  {path: "koszyk", loadChildren: () => import('./cart/cart.module').then(t => t.CartModule)},

  // not-found error
  {path: "nie-znaleziono", component:ErrorNotFoundComponent},
  // internal-server error
  {path: "blad-servera", component:ErrorInternalServerComponent},

  // Finalizacja
  {path: "finalizacja", loadChildren: () => import('./finalization/finalization.module').then(t => t.FinalizationModule)},

  {path: "**", redirectTo: "", pathMatch: "full"}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

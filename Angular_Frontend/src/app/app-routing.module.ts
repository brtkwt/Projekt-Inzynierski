import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ShopComponent } from './shop/shop.component';
import { ContactComponent } from './contact/contact.component';
import { ProductCardComponent } from './shop/product-card/product-card.component';
import { ProductInfoComponent } from './shop/product-info/product-info.component';
import { ErrorNotFoundComponent } from './core/error-not-found/error-not-found.component';
import { ErrorInternalServerComponent } from './core/error-internal-server/error-internal-server.component';
import { AuthorisationGuard } from './core/guards/authorisation.guard';

const routing: Routes = [

  // Strona Głóna
  {path: "", component:HomeComponent, data: {breadcrumb: "Strona Główna"}},
  // Sklep
  {path: "sklep", loadChildren: () => import('./shop/shop.module').then(t => t.ShopModule)},
  // Kontakt
  {path: "kontakt", component:ContactComponent},

  // not-found error
  {path: "nie-znaleziono", component:ErrorNotFoundComponent},
  // internal-server error
  {path: "blad-servera", component:ErrorInternalServerComponent},

  // Koszyk
  {path: "koszyk", loadChildren: () => import('./cart/cart.module').then(t => t.CartModule)},

  // Finalizacja
  {path: "finalizacja", canActivate: [AuthorisationGuard],
   loadChildren: () => import('./finalization/finalization.module').then(t => t.FinalizationModule)},

  // Konto
  {path: "konto", loadChildren: () => import('./account/account.module').then(t => t.AccountModule)},

  // Zamówienia
  {path: "zamowienia", canActivate: [AuthorisationGuard], loadChildren: () => import('./order/order.module').then(m => m.OrderModule)},

  {path: "**", redirectTo: "", pathMatch: "full"}

];

@NgModule({
  imports: [RouterModule.forRoot(routing)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

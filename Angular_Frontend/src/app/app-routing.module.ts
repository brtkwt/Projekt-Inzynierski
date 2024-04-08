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
  {path: "", component:HomeComponent},
  // Sklep
  {path: "sklep", component:ShopComponent},
  // Kontakt
  {path: "kontakt", component:ContactComponent},
  // produkt
  {path: "sklep/:id", component:ProductInfoComponent},

  // not-found error
  {path: "nie-znaleziono", component:ErrorNotFoundComponent},
  // internal-server error
  {path: "blad-servera", component:ErrorInternalServerComponent},

  {path: "**", redirectTo: "", pathMatch: "full"}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

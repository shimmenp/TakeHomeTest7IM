import { NgModule } from "@angular/core";
import { SearchComponent } from "./search.component";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { SearchService } from "../core/services/search.service";
import { SharedModule } from "../shared/shared.module";

@NgModule({
  declarations: [
    SearchComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    SharedModule
  ],
  providers: [SearchService]
})
export class SearchModule { }

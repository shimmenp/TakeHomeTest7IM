import { Component } from "@angular/core";
import { PersonDto } from "../core/models/personDto";
import { SearchService } from "../core/services/search.service";

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent {
  pageTitle: string = 'Person Search';
  searchTerm: string = '';
  searchResults: PersonDto[] = [];
  searchPerformed: boolean = false;
  errorMessage: string = '';
  noSearchTermMessage: string = '';

  constructor(private searchService: SearchService) { }

  searchClicked(): void {
    if (this.searchTerm.trim() !== '') {
      this.searchService.search(this.searchTerm).subscribe(
        {
          next: (results: PersonDto[]) => {
            this.searchResults = results;
            this.searchPerformed = true;
            this.errorMessage = '';
            this.noSearchTermMessage = '';
          },
          error: (error) => {
            console.error('Error searching:', error);
            this.searchResults = [];
            this.searchPerformed = true;
            this.errorMessage = 'An error occurred while searching.';
          }
        }
      );
    } else {
      this.searchResults = [];
      this.searchPerformed = false;
      this.errorMessage = '';
      this.noSearchTermMessage = 'You did not enter a search term, no results returned.';
    }
  }

  textChanged(): void {
    this.searchResults = [];
    this.searchPerformed = false;
    this.errorMessage = '';
    this.noSearchTermMessage = '';
  }
}

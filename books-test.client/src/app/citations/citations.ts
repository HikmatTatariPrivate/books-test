import { Component } from '@angular/core';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import { faEdit, faTrash } from '@fortawesome/free-solid-svg-icons';
import { Citation, CitationsService } from '../../services/citations-service';

@Component({
  selector: 'app-citations',
  standalone: false,
  templateUrl: './citations.html',
  styleUrl: './citations.css',
})
export class Citations {
  faPlus = faPlus;
  faEdit = faEdit;
  faTrash = faTrash;
  citations: Citation[] = [];
  showForm = false;
  newCitation: Citation = {
    id: 0,
    text: '',
    userUsername: ''
  };
  editCitationId: number | null = null;

  constructor(private citationsService: CitationsService) { }

  toggleForm() { this.showForm = true; }


  ngOnInit(): void {
    this.loadCitations();
  }

  loadCitations() {
    this.citationsService.getCitations().subscribe({
      next: (citations) => {
        this.citations = citations
      },
      error: () => {
        alert('Failed to load citations. Please try again.');
      }
    })
  }

  cancelForm() {
    this.showForm = false;
    this.newCitation = { id: 0, text: '', userUsername: '' };
    this.editCitationId = null;

  }

  addNewCitationBtn() {
    this.toggleForm();
  }

  submitForm() {
    if (this.editCitationId == null) {
      this.citationsService.addCitation(this.newCitation).subscribe({
        next: () => {
          this.loadCitations();
          this.cancelForm();
        },
        error: () => {
          alert('Failed to add citation. Please try again.');
          this.cancelForm();
        }
      });
    } else {
      const citation = this.citations.find(c => c.id === this.editCitationId);

      if (citation == null) {
        alert('Failed to edit citation. Please try again.');
        this.cancelForm();
        return;
      }

      this.newCitation = citation;

      this.citationsService.updateCitation(this.editCitationId, this.newCitation).subscribe({
        next: () => {
          this.loadCitations();
          this.cancelForm();
        },
        error: () => {
          alert('Failed to update citation. Please try again.');
          this.cancelForm();
        }
      });
    }
  }

  editCitation(citationId: number) {
    const citation = this.citations.find(c => c.id === citationId);
    if (citation == null) {
      alert('Failed to edit citation. Please try again.');
      return;
    }

    this.editCitationId = citationId;
    this.newCitation = citation;
    this.toggleForm();
  }

  deleteCitation(citationId: number) {
    this.citationsService.deleteCitation(citationId).subscribe({
      next: () => {
        this.loadCitations();
      },
      error: () => {
        alert('Failed to delete citation. Please try again.');
      }
    });
  }

}

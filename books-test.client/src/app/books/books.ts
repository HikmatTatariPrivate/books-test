import { Component } from '@angular/core';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import { faEdit, faTrash } from '@fortawesome/free-solid-svg-icons';
import { Book, BooksService } from '../../services/books-service';

@Component({
  selector: 'app-books',
  standalone: false,
  templateUrl: './books.html',
  styleUrl: './books.css',
})
export class Books {
  faPlus = faPlus;
  faEdit = faEdit;
  faTrash = faTrash;
  books: Book[] = [];
  showForm = false;
  newBook: Book = {
    id: 0,
    title: '',
    author: '',
    price: 0,
    userUsername: ''
  };
  editBookId: number | null = null;

  constructor(private booksService: BooksService) { }

  toggleForm() {this.showForm = true;}


  ngOnInit(): void {
    this.loadBooks();
  }

  loadBooks() {
    this.booksService.getBooks().subscribe({
      next: (books) => {
        this.books = books
      },
      error: () => {
        alert('Failed to load books. Please try again.');
      }
    })
  }

  cancelForm() {
    this.showForm = false;
    this.newBook = { id: 0, title: '', author: '', price: 0, userUsername: '' };
    this.editBookId = null;

  }

  addNewBookBtn() {
    this.toggleForm();
  }

  submitForm() {
    if (this.editBookId == null) {
      this.booksService.addBook(this.newBook).subscribe({
        next: () => {
          this.loadBooks();
          this.cancelForm();
        },
        error: () => {
          alert('Failed to add book. Please try again.');
          this.cancelForm();
        }
      });
    } else {
      const book = this.books.find(b => b.id === this.editBookId);

      if (book == null) {
        alert('Failed to edit book. Please try again.');
        this.cancelForm();
        return;
      }

      this.newBook = book;

      this.booksService.updateBook(this.editBookId, this.newBook).subscribe({
        next: () => {
          this.loadBooks();
          this.cancelForm();
        },
        error: () => {
          alert('Failed to update book. Please try again.');
          this.cancelForm();
        }
      });
    }
  }

  editBook(bookId: number) {
    const book = this.books.find(b => b.id === bookId);
    if (book == null) {
      alert('Failed to edit book. Please try again.');
      return;
    }

    this.editBookId = bookId;
    this.newBook = book ;
    this.toggleForm();
  }

  deleteBook(bookId: number) {
    this.booksService.deleteBook(bookId).subscribe({
      next: () => {
        this.loadBooks();
      },
      error: () => {
        alert('Failed to delete book. Please try again.');
      }
    });
  }

}

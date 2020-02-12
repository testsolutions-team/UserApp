import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { User } from 'src/app/_models/user';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  dataSaved = false;
  userForm: any;
  allUsers: Observable<User[]>;
  UserID = null;
  massage = null;

  constructor(private formbulider: FormBuilder, private userService: UserService) { }
  // UserID, UserName (obligatorio), Name (obligatorio), Email (obligatorio) y Phone (No obligatorio). 
  ngOnInit() {
    this.userForm = this.formbulider.group({
      UserName: ['', [Validators.required]],
      Name: ['', [Validators.required]],
      Email: ['', [Validators.required, Validators.email]],
      Phone: [''],
    });
    this.loadAllUsers();
  }

  loadAllUsers() {
    this.allUsers = this.userService.getUsers();
  }

  onFormSubmit() {
    this.dataSaved = false;
    const user = this.userForm.value;
    this.CreateUser(user);
    this.userForm.reset();
  }

  loadUserToEdit(userId: string) {
    this.userService.getUser(userId).subscribe(user => {
      this.massage = null;
      this.dataSaved = false;
      this.UserID = user.userID;
      this.userForm.controls['UserName'].setValue(user.userName);
      this.userForm.controls['Name'].setValue(user.name);
      this.userForm.controls['Email'].setValue(user.email);
      this.userForm.controls['Phone'].setValue(user.phone);
    }, error => {
      console.log(error);
    });
  }

  CreateUser(user: User) {
    if (this.UserID == null) {
      this.userService.createUser(user).subscribe(
        () => {
          this.massage = 'Record saved Successfully';
          this.dataSaved = true;
          this.loadAllUsers();
          this.UserID = null;
          this.userForm.reset();
        }, error => {
          console.log(error);
        }
      );
    } else {
      user.userID = this.UserID;
      this.userService.updateUser(user).subscribe(() => {
        this.massage = 'Record Updated Successfully';
        this.dataSaved = true;
        this.loadAllUsers();
        this.UserID = null;
        this.userForm.reset();
      }, error => {
        console.log(error);
      });
    }
  }
 
  deleteUser(userId: number, name: string) {
    if (confirm("Are you sure you want to delete this " + name + "?")) {  
    this.userService.deleteUserById(userId).subscribe(() => {
      this.dataSaved = true;
      this.massage = 'Record Deleted Succefully';
      this.loadAllUsers();
      this.UserID = null;
      this.userForm.reset();

      }, error => {
        console.log(error);
      });
    }
  }
  
  resetForm() {
    this.userForm.reset();
    this.massage = null;
    this.dataSaved = false;
  }
}

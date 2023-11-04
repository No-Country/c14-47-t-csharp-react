import { inject } from "@angular/core";
import { AuthService } from "../services/auth.service";
import { Router } from "@angular/router";
import { take, tap } from "rxjs";

export const loginRegisterGuard = () => {

  const authServie = inject(AuthService);
  const router = inject(Router);



  return authServie.getCredential.pipe(take(1), tap((credential)=>{
    credential !== null ? router.navigate(['index']) : true;
  }));
};

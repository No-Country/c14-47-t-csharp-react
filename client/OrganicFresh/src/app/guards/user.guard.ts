import { inject } from "@angular/core";
import { AuthService } from "../services/auth.service";
import { Router } from "@angular/router";


export const userGuard = () => {
  const authService = inject(AuthService)
  const router = inject(Router)
  
  return authService.getCredential.subscribe({
    next:(res)=>{
      (res !== null && res.isAdmin === false)? true : router.navigate(['index']);
    }
  });
};

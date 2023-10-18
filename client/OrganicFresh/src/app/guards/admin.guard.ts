import { AuthService } from '../services/auth.service';
import { inject } from '@angular/core';
import { take, tap } from 'rxjs';
import { Credential } from '../interfaces/credential';
import { Router } from '@angular/router';

export const adminGuard = () => {

  const authService = inject(AuthService);
  
  const router = inject(Router);

  return authService.getCredential.pipe(take(1), tap((credential:Credential|null)=> (credential !== null && credential.isAdmin)? true: router.navigate(['index']) ));


};

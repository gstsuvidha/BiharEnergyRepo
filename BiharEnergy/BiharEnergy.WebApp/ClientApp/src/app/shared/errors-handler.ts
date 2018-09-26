import { Injectable,ErrorHandler } from '@angular/core';
import {MessageService} from 'primeng/components/common/messageservice';



@Injectable()
export class ErrorsHandler implements ErrorHandler{
    constructor(private messageService: MessageService) {}
    
    handleError(error : Error){
        this.messageService.add({
            severity : 'error',
            summary : 'Error Message',
            detail  : 'Check Your Internet Connection'

        });

    }
}
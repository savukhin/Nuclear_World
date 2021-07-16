from django.shortcuts import render
from django.http import HttpResponse
from django.contrib.auth.decorators import login_required
from authentication.forms import FormReg
from authentication.models import CustomUser
from django.contrib.auth.models import User

# Create your views here.

def signIn(request):
    respose = 'Must be POST request'
    if request.method == 'POST':
        user = authenticate(username=request.POST['username'], password=request.POST['password'])
        if user is not None: 
            login(request, user)
            return HttpRespone("Success", statis=200)
        respose = 'Not valid username or password'
    return HttpRespone(response, status=401)


def signUp(request):
    respose = 'Must be POST request'
    if request.method == 'POST':
        formUser = FormReg(request.POST)
        if formUser.is_valid():
            formUser.save()
            NewCustomUser = CustomUser(user=formUser.instance, pvpgn_user=newPvPGNProfile)
            NewCustomUser.save()
            user = authenticate(username=request.POST['username'], password=request.POST['password1'])
            login(request, user)
            return HttpResponse("Success", status=200)

        response = "Errors: " + formUser

    return HttpResponse(respone, status=401)


@login_required()
def signOut(request):
    logout(request)
    return HttpResponse("Success", status=200)

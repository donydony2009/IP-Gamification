git checkout master
git pull origin master
git rm -r --cached .
git add .
git commit -m "fixed untracked files"
git push origin master